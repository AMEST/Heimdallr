<template>
  <div align="center" class="support-block">
    <v-card class="support-card mr-4 mt-2" href="https://github.com/AMEST/Heimdallr/releases/latest">
      <v-icon size="100px">mdi-microsoft</v-icon>
      <v-card-text>Heimdallr CommandLine</v-card-text>
    </v-card>
    <v-card class="support-card mr-4 mt-2" href="https://github.com/AMEST/Heimdallr/releases/latest">
      <v-icon size="100px">mdi-linux</v-icon>
      <v-card-text>Heimdallr CommandLine</v-card-text>
    </v-card>
    <v-card class="support-card mr-4 mt-2" href="/">
      <v-icon size="100px">mdi-web</v-icon>
      <v-card-text>Heimdallr WebApp</v-card-text>
    </v-card>
    <v-card class="support-card mr-4 mt-2" href="https://github.com/AMEST/Heimdallr">
      <v-icon size="100px">mdi-github</v-icon>
      <v-card-text>Fork me on github</v-card-text>
    </v-card>
  </div>
</template>

<script>
export default {
  name: "SupportBlock"
};
</script>

<style>
.support-card {
  width: 125px;
  height: 176px;
  display: inline-block;
  background-color: transparent !important;
  box-shadow: none !important;
  cursor: pointer;
}
.support-card .v-card__text {
  color: rgba(255, 255, 255, 0.7)!important;
  font-weight: bold;
}
.support-card .v-icon{
    color: rgba(255, 255, 255, 0.7)!important;
}

.support-card:hover .v-card__text {
  color: rgba(255, 255, 255)!important;
  font-weight: bold;
}
.support-card:hover .v-icon{
    color: rgba(255, 255, 255)!important;
}
</style>
