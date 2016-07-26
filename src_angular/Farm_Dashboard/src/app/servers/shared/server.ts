export class Server {
  id: number;
  name: string;
  category: string;
}

export class ServerCategory {
  title: string;
  servers: Server[];
}
