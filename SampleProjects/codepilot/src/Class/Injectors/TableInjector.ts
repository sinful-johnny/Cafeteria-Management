import { ITable } from "../Interfaces/ITable";

export class TableInjector{
    private _prototypes = new Map<string, ITable>();

    register(key: string, service: ITable) {
        this._prototypes.set(key, service);
    }

    get(key: string): ITable | undefined {
        return this._prototypes.get(key);
    }
}