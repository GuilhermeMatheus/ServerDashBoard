import { Injectable } from '@angular/core';
import { Server } from './server';

@Injectable()
export class ServersService {

  getServers(category: string) : Server[] {
    let servers : Server[] = [
      { id: 0, name: 'Srv1', category: category },
      { id: 0, name: 'Srv2', category: category },
      { id: 0, name: 'Srv3', category: category },
      { id: 0, name: 'Srv4', category: category }
    ];
    return servers;
  }

  getServersByCategory() {
    return [
      { title: 'Produção', servers: this.getServers('Produção') },
      { title: 'Desenvolvimento', servers: this.getServers('Desenvolvimento') },
      { title: 'Homologação', servers: this.getServers('Homologação') }
    ];
  }
}
