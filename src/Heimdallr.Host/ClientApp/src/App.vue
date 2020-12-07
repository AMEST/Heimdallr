<template>
  <v-app class="bg">
    <v-app-bar class="bar-transparent" app dark dense elevate-on-scroll>
      <v-container>
        <v-toolbar-title>
          <img src="/img/icons/android-chrome-512x512.png" style="vertical-align: middle;" width="32px"/>
          Heimdallr
        </v-toolbar-title>
      </v-container>
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
    //
  })
};
</script>

<style>
.max-height {
  height: calc(100vh - 54px) !important;
}
.bg {
  background: url(/img/bg.21335f0f.svg) no-repeat fixed !important;
  background-size: cover !important;
}
.bar-transparent {
  background-color: rgba(0, 0, 0, 0.4) !important;
}
</style>
