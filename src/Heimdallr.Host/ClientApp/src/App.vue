<template>
  <v-app class="bg">
    <v-app-bar class="bar-transparent" app dark dense elevate-on-scroll>
        <v-toolbar-title>
          <img
            src="/img/icons/android-chrome-512x512.png"
            style="vertical-align: middle;"
            width="32px"
          />
          Heimdallr
        </v-toolbar-title>
        <v-spacer></v-spacer>
        <div style="color:rgba(255, 255, 255, 0.8);">
          Version: {{this.version}}
        </div>
    </v-app-bar>

    <v-main>
      <router-view />
    </v-main>
  </v-app>
</template>

<script>
export default {
  name: "App",

  data: () => ({
    version: ""
  }),
  created: function() {
    var self = this;
    var request = new XMLHttpRequest();
    request.open("GET", "/api/version");
    request.setRequestHeader("Content-Type", "application/json");
    request.onload = function() {
      var result = JSON.parse(request.response);
      self.version = result.version;
    };
    request.send();
  }
};
</script>

<style>
.max-height {
  height: calc(100vh - 54px) !important;
}
.bg {
    background: -webkit-linear-gradient(top, rgb(43, 69, 159) 0%, rgb(27, 121, 232) 100%) !important;
    background: -o-linear-gradient(top, rgb(43, 69, 159) 0%, rgb(27, 121, 232) 100%) !important;
    background: -ms-linear-gradient(top, rgb(43, 69, 159) 0%, rgb(27, 121, 232) 100%) !important;
    background: -moz-linear-gradient(top, rgb(43, 69, 159) 0%, rgb(27, 121, 232) 100%) !important;
    background: linear-gradient(to bottom, rgb(43, 69, 159) 0%, rgb(27, 121, 232) 100%) !important;
}
.bar-transparent {
  background-color: rgba(0, 0, 0, 0.4) !important;
}
</style>
