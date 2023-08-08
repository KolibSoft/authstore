import { Catalogue, IDBUtils } from "../lib/main.js";

class AuthStoreContext extends Catalogue.DbContext {

    onUpgrade(database) {
        IDBUtils.buildAuthStore(database);
    }

    constructor() {
        super("authstore", 1);
    }

}

export {
    AuthStoreContext
}