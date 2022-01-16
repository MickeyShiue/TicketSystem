<template>
    <div class="form-group row">
        <div class="col-12">
            <a href="/TicketCreate" class="btn btn-primary mr-3" v-if="role!='RD'" @click="gotoCreate">Create</a>
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
    import { checkLogin, api, token, mapTicketData, mapRole } from '../Common/Common'
    import axios from 'axios'
    export default {
        name: "TicketList",
        data() {
            return {
                tickets: [],
                role: ''
            }
        },
        methods: {
            getTickets() {
                var _this = this
                axios({
                    method: 'get',
                    url: api.getTickets,
                    headers: {
                        Authorization: token
                    }
                }).then(function (response) {
                    if (response.data.errorCode == 200) {
                        _this.tickets = mapTicketData(response.data.data.tickets);                       
                    } else {
                        alert('getTicketFail');
                    }
                });
            },
            getUserRole() {
                var _this = this
                axios({
                    method: 'get',
                    url: api.getUserRole,
                    headers: {
                        Authorization: token
                    }
                }).then(function (response) {
                    if (response.data.errorCode == 200) {
                        _this.role = mapRole(response.data.data);
                    }
                });
            }           
        },
        mounted() {
            checkLogin()
            this.getTickets();
            this.getUserRole();
        }
    }
</script>