/**
 * 
 * @param {IDBDatabase} database 
 */
function buildAuthStore(database) {
    database.createObjectStore("credential", { keyPath: "id" });
    database.createObjectStore("permission", { keyPath: "id" });
    database.createObjectStore("credential_permission", { keyPath: "id" });
}

const IDBUtils = {
    buildAuthStore
};

export {
    IDBUtils
}