using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace HostDoctor.Diagnostics.Service.TPL
{
    public abstract class NeverEndTask<TMessage>
    {
        private CancellationTokenSource _wToken;
        private ITargetBlock<TMessage> _task;

        protected abstract Task DoWorkAsync(TMessage message, CancellationToken cancellationToken);
        protected abstract TMessage Seed();
        protected abstract TimeSpan GetPreferredUpdateTime();

        private ITargetBlock<TMessage> CreateNeverEndingTask(CancellationToken cancellationToken)
        {
            ActionBlock<TMessage> block = null;

            block =
                new ActionBlock<TMessage>(async message => {
                    await DoWorkAsync(message, cancellationToken)
                            .ConfigureAwait(false);

                    await Task
                            .Delay(GetPreferredUpdateTime(), cancellationToken)
                            .ConfigureAwait(false);

                    block.Post(Seed());
                },
                new ExecutionDataflowBlockOptions { CancellationToken = cancellationToken }
            );

            return block;
        }

        public void StartWork()
        {
            _wToken = new CancellationTokenSource();
            _task = CreateNeverEndingTask(_wToken.Token);
            _task.Post(Seed());
        }

        public void StopWork()
        {
            using (_wToken)
                _wToken.Cancel();

            _wToken = null;
            _task = null;
        }
    }
}
