<template>
  <v-container grid-list-md text-xs-center>
    <v-layout row wrap>
      <v-flex xs12>
        <v-card dark color="primary">
          <form action="account/register" method="post" class="form-group">
            <input type="text" name="Email" value="admin@bestdealpharma.com" class="form-control" />
            <input type="text" name="Password" value="Q1w2e3!" class="form-control" />
            <input type="submit" value="Register" class="btn btn-info" />
          </form>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>
<script>
  export default {
    data() {
      return {}
    }
  }
</script>
<style>
</style>
