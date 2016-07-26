import { Injectable } from '@angular/core';
import { Server } from './server';

@Injectable()
export class ServersService {

  getServers(category: string) : Server[] {
    let servers : Server[] = [
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
