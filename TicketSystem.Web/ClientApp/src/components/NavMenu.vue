<template>
    <header>
        <nav class="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
            <div class="container">
                <a class="navbar-brand">Vue JS Template for .NET 5</a>               
                <div class="navbar-collapse collapse d-sm-inline-flex flex-sm-row-reverse" v-bind:class="{show: isExpanded}" v-if="isLogin">
                    <ul class="navbar-nav flex-grow">                                    
                        <li class="nav-item">
                            <button type="button" class="btn btn-danger" @click="Logout">Logout</button>
                        </li>                      
                    </ul>
                </div>
            </div>
        </nav>
    </header>
</template>

<style>
    a.navbar-brand {
        white-space: normal;
        text-align: center;
        word-break: break-all;
    }

    html {
        font-size: 14px;
    }

    @media (min-width: 768px) {
        html {
            font-size: 16px;
        }
    }

    .box-shadow {
        box-shadow: 0 .25rem .75rem rgba(0, 0, 0, .05);
    }
</style>
<script>
    import { isShowLogOutButton,logOut } from '../Common/Common'
    export default {
        name: "NavMenu",
        data() {
            return {               
                isLogin: false
            }
        },
        methods: {
            Logout() {
                logOut();
            }
        },
        mounted() {            
            this.isLogin = isShowLogOutButton();                   
        }
    }
</script>