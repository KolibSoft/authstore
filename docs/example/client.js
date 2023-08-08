import { Catalogue, IDBUtils } from "../lib/main.js";

class AuthStoreContext extends Catalogue.DbContext {

    constructor() {
        super("authstore", 1);
    }

    onUpgrade(database) {
        IDBUtils.buildAuthStore(database);
    }
}

export {
    AuthStoreContext
}