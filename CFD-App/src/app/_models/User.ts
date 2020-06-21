import { Renda } from './Renda';
import { Divida } from './Divida';

export class User {
  id: number;
  nome: string;
  email: string;
  senha: string;
  papel: number;
  Dividas: Divida[];
  Rendas: Renda[];
}
