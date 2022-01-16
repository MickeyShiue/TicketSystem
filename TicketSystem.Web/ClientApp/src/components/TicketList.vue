<template>
    <div class="form-group row">
        <div class="col-12">
            <button type="button" class="btn btn-primary mr-3" v-if="role!=2" @click="gotoCreate">Create</button>
        </div>
    </div>
    <table class='table table-striped' aria-labelledby="tableLabel" v-if="tickets">
        <thead>
            <tr>
                <th>Title</th>
                <th>Description</th>
                <th>type</th>
                <th>Status</th>
                <th>Edit</th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="ticket in tickets" v-bind:key="ticket">
                <td>{{ ticket.title }}</td>
                <td>{{ ticket.description }}</td>
                <td>{{ ticket.ticketType }}</td>
                <td>{{ ticket.ticketStatus }}</td>
                <td><a :href="`/TicketEdit?ticketId=${ticket.ticketId}`">Edit</a></td>
            </tr>
        </tbody>
    </table>
</template>

<script>
    import { IsLogin, sessionStorageKeys, api, mapTicketType, mapTicketStatus } from '../Common/Common'
    import axios from 'axios'
    export default {
        name: "TicketList",
        data() {
            return {
                tickets: [],
                role:''
            }
        },
        methods: {
            getTickets() {
                var _this = this
                axios({
                    method: 'post',
                    url: api.getTickets,
                    headers: {
                        Authorization: 'Bearer ' + sessionStorage.getItem(sessionStorageKeys.token)
                    }
                }).then(function (response) {
                    if (response.data.errorCode == 200) {                        
                        _this.tickets = response.data.data.tickets;

                        _this.tickets.forEach(function(item){
                            item.ticketType = mapTicketType(item.ticketType);
                            item.ticketStatus = mapTicketStatus(item.ticketStatus);                            
                        });

                        console.log(_this.tickets);
                    } else {
                        alert('getTicketFail');
                        console.log(response.data);
                    }
                });
            },
            getUserRole() {
                var _this = this
                axios({
                    method: 'post',
                    url: api.getUserRole,
                    headers: {
                        Authorization: 'Bearer ' + sessionStorage.getItem(sessionStorageKeys.token)
                    }
                }).then(function (response) {
                    if (response.data.errorCode == 200) {
                        _this.role = response.data.data.role;            
                    } 
                });
            },
            gotoCreate() {
                window.location = '/TicketCreate'
            }
        },
        mounted() {         
            if (!IsLogin()) {
                window.location = '/'
            }
            this.getTickets();
            this.getUserRole();
        }
    }
</script>