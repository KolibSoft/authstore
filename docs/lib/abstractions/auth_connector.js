/**
 * @interface
 */
class AuthConnector {

    /** @returns {boolean} */
    get available() { return new Error("Not implemented"); }

    accesAsync(login) { return new Error("Not implemented"); }

    refreshAsync(id, refreshToken) { return new Error("Not implemented"); }

}

export {
    AuthConnector
}