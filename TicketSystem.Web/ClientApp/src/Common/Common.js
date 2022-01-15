export const api =
{ 
    loginUrl:'api/Token/Login',
}

export const sessionStorageKeys = {
    token:'JwtToken',
}

export function IsLogin() {
    var token = sessionStorage.getItem(sessionStorageKeys.token);  
    if (token === null) return false    
        return true        
}