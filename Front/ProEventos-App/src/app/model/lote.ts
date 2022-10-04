import { Evento } from "./evento";

export interface Lote {
  id: number ;
  nome: string ;
  preco: number ;
  dateInicio?: Date ;
  dataFim?: Date ;
  quantidade: number ;
  eventoId: number ;
  evento: Evento ;
}
