using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace HostDoctor.Diagnostics.Service.TPL
{
    public class DoctorNeverEndTask : NeverEndTask<ExamResult>
    {
        private readonly IExam _examToPerform;
        private readonly List<ActionBlock<ExamResult>> _actions;
        
        public DoctorNeverEndTask(IExam examToPerform)
        {
            if (examToPerform == null)
                throw new ArgumentNullException("examToPerform");

            _examToPerform = examToPerform;
            _actions = new List<ActionBlock<ExamResult>>();
        }

        public void AddActionBlock(ActionBlock<ExamResult> block)
        {
            if (block == null)
                throw new ArgumentNullException("block");
            _actions.Add(block);
        }

        public void AddActionBlock(IEnumerable<ActionBlock<ExamResult>> blocks)
        {
            if (blocks == null)
                throw new ArgumentNullException("blocks");
            _actions.AddRange(blocks);
        }

        protected override ExamResult Seed()
        {
            return _examToPerform.GetResult();
        }

        protected override TimeSpan GetPreferredUpdateTime()
        {
            return _examToPerform.GetPreferredUpdateTime();
        }

        protected override async Task DoWorkAsync(ExamResult message, CancellationToken cancellationToken)
        {
            await Task.Run(() => {
                foreach (var item in _actions)
                    item.Post(message);
            }, cancellationToken);
        }
    }
}
