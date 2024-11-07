import { ITable } from "../Interfaces/ITable";
import { RectangleTable } from '../Tables/RectangleTable';
import { CircleTable } from '../Tables/CircleTable';


export class TableFactory {
    static createTable(id: number, shapeId: String,type: string, x: number, y: number, width: number, height: number, radius: number): ITable {
      switch (type) {
        case 'rectangle':
          return new RectangleTable(id, shapeId,x, y, width, height, "unlocked");
        case 'circle':
          return new CircleTable(id, shapeId,x, y, radius, "unlocked");
        default:
          throw new Error('Unknown table type');
      }
    }
  }