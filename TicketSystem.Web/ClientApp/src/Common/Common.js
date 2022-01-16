export const api = {
    loginUrl: 'api/Token/Login',
    getUserRole: 'api/Role/UserRole',
    getTickets: 'api/Ticket/TicketList',
    getTicketById: 'api/Ticket/TicketById',
    createTicket: 'api/Ticket/CreateTicket',
    updateTicket: 'api/Ticket/UpdateTicket',
    deleteTicket: 'api/Ticket/DeleteTicket',
    getTicketStatus: 'api/Ticket/GetTicketStatus'
}



export const sessionStorageKeys = {
    token: 'JwtToken',
}

export const token = 'Bearer ' + sessionStorage.getItem(sessionStorageKeys.token);

export function checkLogin() {
    var token = sessionStorage.getItem(sessionStorageKeys.token);
    if (token === null) window.location = '/'
}

export function isShowLogOutButton() {
    var token = sessionStorage.getItem(sessionStorageKeys.token);
    if (token === null) return false;
    return true;
}

export function loging(token) {
    sessionStorage.setItem(sessionStorageKeys.token, token);
    window.location = '/TicketList';
}

export function logOut() {
    sessionStorage.removeItem(sessionStorageKeys.token);
    window.location = '/'
}

export function mapTicketData(tickes) {
    tickes.forEach(function (ticket) {
        ticket.ticketType = mapTicketType(ticket.ticketType);
        ticket.ticketStatus = mapTicketStatus(ticket.ticketStatus);
    });

    return tickes;
}

export function mapRole(role) {

    if (role == 1) {
        return 'QA'
    } else if (role == 2) {
        return 'RD'
    } else if (role == 3) {
        return 'PM'
    } else if (role == 4) {
        return 'Admin'
    } else {
        return 'Default'
    }
}


function mapTicketType(type) {

    if (type == 1) {
        return 'Bug'
    } else if (type == 2) {
        return 'Feature'
    } else {
        return 'Test'
    }
}

function mapTicketStatus(status) {

    if (status == 1) {
        return 'New'
    } else if (status == 2) {
        return 'Active'
    } else if (status == 3) {
        return 'Resloved'
    } else {
        return 'Closed'
    }
}