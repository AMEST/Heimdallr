<template>
  <v-container style="padding: 0px;" class="pt-4 full-height">
    <v-row align="center">
      <v-col class="col-lg-6 col-md-6 col-sm-12 col-xs-12 col-12">
        <PasswordGenerate />
      </v-col>
      <v-col class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
        <InfoBlock/>
        <SupportBlock/>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import PasswordGenerate from "../components/PasswordGenerate"
import InfoBlock from "../components/InfoBlock"
import SupportBlock from "../components/SupportBlock"

export default {
  name: "Home",
  components: {
    PasswordGenerate,
    InfoBlock,
    SupportBlock
  }
};
</script>

<style>
.full-height {
  min-height: calc(100vh - 50px) !important;
}

</style>