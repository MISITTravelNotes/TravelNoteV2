const vm = Vue.createApp({
    data() {
        return {
            account: '',
            password: '',
            password2: '',
            errorMessages: [],
            logInMessage: '',
        }
    },
    methods: {
        Register() {
            this.errorMessages = [];
            console.log(this.account, this.password, this.password2);

            if (this.ValidateForm() && this.ValidatePassword()) {
                if (this.password === this.password2) {
                    axios.post('api/MemberApi/Register', {
                        email: this.account,
                        password: this.password
                    })
                        .then(reponse => {
                            console.log("OK");
                        })
                        .catch(error => {
                            console.error('Error during close', error);
                        })
                } else {
                    this.errorMessages.push('確認密碼不一致!');
                }
            }
        },
        Login() {
            console.log(this.account, this.password)
            this.errorMessages = [];
            if (this.ValidateForm() && this.ValidatePassword()) {
                axios.get(`api/MemberApi/GetLoginToken?email=${this.account}&password=${this.password}`)
                    .then(response => {
                        this.logInMessage += response.data;
                    })
                    .catch(error => {
                        alert('登入失敗!');
                        console.error('Error during close', error);
                    });
                console.log("OK");
            } else {
                console.log("Not OK");
            }
        },
        ValidateForm() {
            var re = /^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z]+$/;
            if (this.account === '') {
                this.errorMessages.push('登入信箱不能為空!');
                return false;
            } else if (!re.test(this.account)) {
                this.errorMessages.push('電子信箱格式不正確');
                return false;
            }
            return true;
        },
        ValidatePassword() {
            if (this.password.length < 8 || this.password.length > 20) {
                this.errorMessages.push('密碼必須8~20位!');
                return false;
            }
            return true;
        }

    },
    mounted(){
    }

}).mount('#app');

