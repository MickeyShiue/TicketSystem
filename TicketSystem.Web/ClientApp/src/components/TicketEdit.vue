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
                <option v-for="option in options" v-bind:value="option.id" v-bind:key="option.id">
                    {{ option.value }}
                </option>
            </select>
        </div>
    </div>
    <div class="form-group row">
        <div class="col-12">
            <button type="button" class="btn btn-primary mr-3" @click="updateTicket">update</button>
        </div>
    </div>
</template>

<script>
    import { IsLogin, sessionStorageKeys, api } from '../Common/Common'
    import axios from 'axios'
    export default {
        name: "TicketEdit",
        data() {
            return {
                ticketId: '',
                title: '',
                description: '',
                ticketStatus: '',
                ticketType: '',
                options: [],
            }
        },
        methods: {
            getTicketById() {
                var _this = this
                let ticketId = location.toString().replace('http://localhost:5000/TicketEdit?ticketId=', '').toString();
                axios({
                    method: 'post',
                    url: api.getTicketById,
                    data: {
                        ticketId: ticketId
                    },
                    headers: {
                        Authorization: 'Bearer ' + sessionStorage.getItem(sessionStorageKeys.token)
                    }
                }).then(function (response) {
                    if (response.data.errorCode == 200) {
                        let responseData = response.data.data;
                        _this.options = responseData.ticketStatusOptions
                        _this.ticketId = responseData.ticketId;
                        _this.title = responseData.title;
                        _this.description = responseData.description;
                        _this.ticketType = responseData.ticketType;
                        _this.ticketStatus = responseData.ticketStatus;
                    } else {
                        alert('GetTicketById');
                        console.log(response.data);
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
                        Authorization: 'Bearer ' + sessionStorage.getItem(sessionStorageKeys.token)
                    }
                }).then(function (response) {
                    if (response.data.errorCode == 200) {
                        alert('update ticket success')
                        window.location = '/TicketList'
                    } else {
                        alert('updateTicket');
                        console.log(response.data);
                    }
                });
            }
        },
        mounted() {
            if (!IsLogin()) {
                window.location = '/'
            }
            this.getTicketById();
        }
    }
</script>