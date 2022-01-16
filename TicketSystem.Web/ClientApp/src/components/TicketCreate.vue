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
            <select  v-model="ticketStatus" class="form-control sl">
                <option v-for="option in options" v-bind:value="option.id" v-bind:key="option.id">
                    {{ option.value }}
                </option>
            </select>
        </div>
    </div>
    <div class="form-group row">
        <div class="col-12">
            <button type="button" class="btn btn-primary mr-3" @click="createTicket">Create</button>
        </div>
    </div>
</template>

<script>
    import { IsLogin, sessionStorageKeys, api } from '../Common/Common'
    import axios from 'axios'
    export default {
        name: "TicketCreate",
        data() {
            return {              
                title: '',
                description: '',
                ticketStatus: '',                
                options: [],
            }
        },
        methods: {           
            getTicketStatus() {
                var _this = this
                axios({
                    method: 'post',
                    url: api.getTicketStatus,
                    headers: {
                        Authorization: 'Bearer ' + sessionStorage.getItem(sessionStorageKeys.token)
                    }
                }).then(function (response) {
                    if (response.data.errorCode == 200) {
                        _this.options = response.data.data.ticketStatusOptions;                        
                    }
                });
            },
            createTicket() {
                var _this = this
                axios({
                    method: 'post',
                    url: api.createTicket,
                    data: {
                        title: _this.title,
                        description: _this.description,
                        ticketStatus: _this.ticketStatus
                    },
                    headers: {
                        Authorization: 'Bearer ' + sessionStorage.getItem(sessionStorageKeys.token)
                    }
                }).then(function (response) {
                    if (response.data.errorCode == 200) {
                        alert('create ticket success');
                        window.location = '/TicketList'
                    }
                });
            }
        },
        mounted() {
            if (!IsLogin()) {
                window.location = '/'
            }
            this.getTicketStatus();
        }
    }
</script>