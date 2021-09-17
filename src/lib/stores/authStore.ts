import { writable } from "svelte/store";

export const isLoggedIn =  writable(false);

export async function refreshAuthStatus(){
    const response = await fetch('/.auth/me');
    const payload = await response.json();
    const { clientPrincipal } = payload;

    isLoggedIn.set(clientPrincipal?.userRoles.indexOf('authenticated') >= 0);
}