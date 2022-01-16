export const api =
{
    loginUrl: 'api/Token/Login',
    getTickets: 'api/Ticket/TicketList',
    getTicketById: 'api/Ticket/GetTicketById',
    updateTicket: 'api/Ticket/UpdateTicket',
    deleteTicket: 'api/Ticket/DeleteTicket',
    getTicketStatus: 'api/Ticket/GetTicketStatus',
    createTicket: 'api/Ticket/CreateTicket',
    getUserRole:'api/Role/UserRole'
}

export const sessionStorageKeys = {
    token: 'JwtToken',
}

export function IsLogin() {
    var token = sessionStorage.getItem(sessionStorageKeys.token);
    if (token === null) return false
    return true
}


export function mapTicketType(type) {

    if (type == 1) {
        return 'Bug'
    } else if (type == 2) {
        return 'Feature'
    } else {
        return 'Test'
    }
}

export function mapTicketStatus(status) {

    if (status == 1) {
        return 'New'
    } else if (status == 2) {
        return 'Active'
    } else if (status == 3){
        return 'Resloved'
    } else {
        return 'Closed'
    }
}