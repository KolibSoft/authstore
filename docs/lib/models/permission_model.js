import { Item } from "../modules/catalogue.js";

class PermissionModel extends Item{

    /** @type {string} */
    code;

    /** @type {boolean} */
    active;

    constructor(json) {
        this.code = json?.code ?? "";
        this.active = json?.active ?? true;
    }

}

export {
    PermissionModel
}