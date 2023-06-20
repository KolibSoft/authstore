class AuthResponse {
    constructor(json) {
        this.auth = json?.auth ?? null;
        this.errors = json?.errors ?? [];
    }
}

export {
    AuthResponse
}