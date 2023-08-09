class IDBUtils {

    /**
     * @param {IDBDatabase} database 
     */
    static buildAuthStore(database) {
        database.createObjectStore("credential", { keyPath: "id" });
        database.createObjectStore("permission", { keyPath: "id" });
        database.createObjectStore("credential_permission", { keyPath: "id" });
    }

}

export {
    IDBUtils
}