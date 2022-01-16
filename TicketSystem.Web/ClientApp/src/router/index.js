import { createWebHistory, createRouter } from "vue-router";
import Login from "@/components/Login.vue";
import TicketList from "@/components/TicketList.vue";
import TicketCreate from "@/components/TicketCreate.vue";
import TicketEdit from "@/components/TicketEdit.vue";

const routes = [
    {
        path: "/",
        name: "Login",
        component: Login,
    },      
    {
        path: "/TicketList",
        name: "TicketList",
        component: TicketList,
    },   
    {
        path: "/TicketCreate",
        name: "TicketCreate",
        component: TicketCreate,
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