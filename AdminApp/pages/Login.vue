<template>
  <v-app id="login" class="primary">
    <v-content>
      <v-container fluid fill-height>
        <v-layout align-center justify-center>
          <v-flex xs12 sm8 md4 lg4>
            <v-card class="elevation-1 pa-3">
              <v-card-text>
                <div class="layout column align-center">
                  <img src="/images/logo.png" alt="bestdealpharma.com" width="240">
                  <h1 class="flex my-4 primary--text">Management Panel</h1>
                </div>
                <v-form>
                  <v-text-field append-icon="person" name="login" label="Email" type="text" v-model="model.email"></v-text-field>
                  <v-text-field append-icon="lock" name="password" label="Password" id="password" type="password"
                                v-model="model.password"></v-text-field>
                </v-form>
              </v-card-text>
              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn color="primary" @click="login" :loading="loading">Login</v-btn>
              </v-card-actions>
            </v-card>
          </v-flex>
        </v-layout>
      </v-container>
    </v-content>
  </v-app>
</template>

<script>
  export default {
    data: () => ({
      loading: false,
      model: {
        email: null,
        password: null,
        forAdminPanel: true
      }
    }),

    methods: {
      login() {
        var self = this
        this.loading = true
        this.axios.post("/account/login", JSON.stringify(this.model), {
          headers: {
            'Content-Type': 'application/json',
          }
        }).then(
          response => {
            self.$myLocalStorage.setEnc('user', response.data.user)
            window.localStorage.setItem('token', response.data.token)
            window.location.href = response.data.returnUrl
          }
        ).catch(err => {
          this.loading = false
          console.log(err)
        })
      }
    }
  }
</script>
<style scoped lang="css">
  #login {
    height: 50%;
    width: 100%;
    position: absolute;
    top: 0;
    left: 0;
    content: "";
    z-index: 0;
  }
</style>
