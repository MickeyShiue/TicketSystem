<template>
    <h1>TicketList</h1>
</template>


<script>
    export default {
        name: "TicketList",
        data() {
            return {
                currentCount: 0
            }
        },
        methods: {
            incrementCounter() {
                this.currentCount++;
            }
        }
    }
</script>