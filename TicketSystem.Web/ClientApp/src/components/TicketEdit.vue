<template>
    <div class="form-group row">
        <label class="col-4 col-form-label" for="Title">title</label>
        <div class="col-4">
            <input type="text" class="form-control" v-model="title">
        </div>
    </div>
    <div class="form-group row">
        <label for="" class="col-4 col-form-label">description</label>
        <div class="col-4">
            <input type="text" class="form-control" v-model="description">
        </div>
    </div>
    <div class="form-group row">
        <label for="" class="col-4 col-form-label">Status</label>
        <div class="col-4">
            <select v-model="ticketStatus" class="form-control sl">
                <option v-for="option in ticketStatusOptions" v-bind:value="option.id" v-bind:key="option.id">
                    {{ option.value }}
                </option>
            </select>
        </div>
    </div>
    <div class="form-group row">
        <div class="col-12">
            <button type="button" class="btn btn-primary mr-3" @click="updateTicket">updateTicket</button>
            <button type="button" class="btn btn btn-danger mr-3" @click="deleteTicket" v-if="role=='QA'" >DeleteTicket</button>
        </div>
    </div>
</template>

<script>
    import { checkLogin, token, api, getQueryString, mapRole} from '../Common/Common'
    import axios from 'axios'
    export default {
        name: "TicketEdit",
        data() {
            return {
                ticketId: '',
                title: '',
                description: '',
                ticketStatus: '',                
                role:'',
                ticketStatusOptions: [],
            }
        },
        methods: {
            getTicketById() {
                var _this = this            
                axios({
                    method: 'post',
                    url: api.getTicketById,
                    data: {
                        ticketId: getQueryString('ticketId')
                    },
                    headers: {
                        Authorization: token
                    }
                }).then(function (response) {
                    if (response.data.errorCode == 200) {
                        let responseData = response.data.data;                                                                 
                        _this.ticketId = responseData.ticketId;
                        _this.title = responseData.title;
                        _this.description = responseData.description;                        
                        _this.ticketStatus = responseData.ticketStatus;
                    } else {
                        alert('GetTicketById');                     
                    }
                });
            },
            getTicketStatus() {
                var _this = this
                axios({
                    method: 'post',
                    url: api.getTicketStatus,
                    headers: {
                        Authorization: token
                    }
                }).then(function (response) {
                    if (response.data.errorCode == 200) {
                        _this.ticketStatusOptions = response.data.data.ticketStatusOptions;
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
            },
            updateTicket() {
                var _this = this
                axios({
                    method: 'post',
                    url: api.updateTicket,
                    data: {
                        ticketId: _this.ticketId,
                        title: _this.title,
                        description: _this.description,
                        ticketStatus: _this.ticketStatus
                    },
                    headers: {
                        Authorization: token
                    }
                }).then(function (response) {
                    if (response.data.errorCode == 200) {
                        alert('update ticket success')
                        window.location = '/TicketList'
                    } else {
                        alert('updateTicket fail');                     
                    }
                });
            },
            deleteTicket() {
                var _this = this
                axios({
                    method: 'post',
                    url: api.deleteTicket,
                    data: {
                        ticketId: _this.ticketId                     
                    },
                    headers: {
                        Authorization: token
                    }
                }).then(function (response) {
                    if (response.data.errorCode == 200) {
                        alert('delete ticket success')
                        window.location = '/TicketList'
                    } else {
                        alert('deleteTicket fail');
                    }
                });
            }
        },
        mounted() {            
            checkLogin()
            this.getTicketById();
            this.getTicketStatus();
            this.getUserRole();
        }
    }
</script>