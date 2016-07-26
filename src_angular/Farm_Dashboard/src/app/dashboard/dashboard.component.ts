import { Component, OnInit } from '@angular/core';
import { ServersService } from '../servers/shared/servers.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './app/dashboard/dashboard.component.html',
  styleUrls: ['./app/dashboard/dashboard.component.css']
})
export class DashboardComponent {

  public serversByCategories;

  constructor(
    private serversService: ServersService ) {
      this.serversByCategories = serversService.getServersByCategory();
  }
}
