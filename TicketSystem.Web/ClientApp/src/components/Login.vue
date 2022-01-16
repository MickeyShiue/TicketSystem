<template>
    <section class="vh-100" style="background-color: #508bfc;">
        <div class="container py-5 h-100">
            <div class="row d-flex justify-content-center align-items-center h-100">
                <div class="col-12 col-md-8 col-lg-6 col-xl-5">
                    <div class="card shadow-2-strong" style="border-radius: 1rem;">
                        <div class="card-body p-5 text-center">
                            <h2 class="mb-5">Login</h2>
                            <div class="form-outline mb-4">
                                <input type="email" id="typeEmailX-2" class="form-control form-control-lg" v-model="userName" />
                                <label class="form-label" for="typeEmailX-2">UserName</label>
                            </div>
                            <div class="form-outline mb-4">
                                <input type="password" id="typePasswordX-2" class="form-control form-control-lg" v-model="passWord" />
                                <label class="form-label" for="typePasswordX-2">Password</label>
                            </div>
                            <button class="btn btn-primary btn-lg btn-block" @click="Login">Login</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</template>

<script>
    import axios from 'axios'
    import { api, loging} from '../Common/Common'
    export default {
        name: "Login",
        data() {
            return {
                userName: '',
                passWord: '',            
            }
        },
        methods: {
            Login() {
                axios({
                    method: 'post',
                    url: api.loginUrl,
                    data: {
                        Username: this.userName,
                        PassWord: this.passWord
                    }
                }).then(function (response) {                    
                    if (response.data.errorCode == 200) {
                        loging(response.data.data.token);                        
                    } else {
                        alert('LoginFail');                       
                    }
                });
            }
        }        
    }
</script>