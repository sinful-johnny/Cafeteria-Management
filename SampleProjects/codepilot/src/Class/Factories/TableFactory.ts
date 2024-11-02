import { ITable } from "../Interfaces/ITable";
import { RectangleTable } from '../Tables/RectangleTable';
import { CircleTable } from '../Tables/CircleTable';


export class TableFactory {
    static createTable(type: string, x: number, y: number): ITable {
      switch (type) {
        case 'rectangle':
          return new RectangleTable(x, y, 100, 100, "#D9D9D9");
        case 'circle':
          return new CircleTable(x, y, 50, "#D9D9D9");
        default:
          throw new Error('Unknown table type');
      }
    }
  }