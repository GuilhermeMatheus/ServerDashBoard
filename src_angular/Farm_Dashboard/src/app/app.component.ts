import { Component } from '@angular/core';
import { ROUTER_DIRECTIVES }  from '@angular/router';
import { ServersService } from './servers/shared/servers.service';

@Component({
  moduleId: module.id,
  selector: 'app-root',
  templateUrl: 'app.component.html',
  providers: [ ServersService ],
  directives: [ ROUTER_DIRECTIVES ],
  styleUrls: ['app.component.css']
})
export class AppComponent {
  title = 'app works!';
}
