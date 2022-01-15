<template>
    <table class='table table-striped' aria-labelledby="tableLabel" v-if="Tickets">
        <thead>
            <tr>
                <th>Title</th>
                <th>Description</th>
                <th>Status</th>
                <th>operating</th>
            </tr>
        </thead>
        <tbody>
            <!--<tr v-for="forecast of forecasts" v-bind:key="forecast">
                <td>{{ forecast.date }}</td>
                <td>{{ forecast.temperatureC }}</td>
                <td>{{ forecast.temperatureF }}</td>
                <td>{{ forecast.summary }}</td>
            </tr>-->
        </tbody>
    </table>
</template>


<script>
    import { IsLogin } from '../Common/Common'
    export default {
        name: "TicketList",
        data() {
            return {
                Tickets:[]
              
            }
        },
        methods: {
            incrementCounter() {
                
            }
        },
        mounted() {         
            if (!IsLogin()) {
                window.location = '/'
            }            
        }
    }
</script>