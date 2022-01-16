import { createWebHistory, createRouter } from "vue-router";
import FetchData from "@/components/FetchData.vue";
import TicketList from "@/components/TicketList.vue";
import TicketEdit from "@/components/TicketEdit.vue";
import Login from "@/components/Login.vue";

const routes = [
    {
        path: "/",
        name: "Login",
        component: Login,
    },   
    {
        path: "/FetchData",
        name: "FetchData",
        component: FetchData,
    },
    {
        path: "/TicketList",
        name: "TicketList",
        component: TicketList,
    },
    {
        path: "/TicketEdit",
        name: "TicketEdit",
        component: TicketEdit,
    }
];

const router = createRouter({
    history: createWebHistory(),
    routes,
});

export default router;