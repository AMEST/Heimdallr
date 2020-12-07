<template>
  <div align="center" class="info-block">
    <h2>
      What is Heimdallr?
      <br />Heimdall is a stateless password manager
    </h2>
    <h4>
      You just need to remember one master password and thanks to Heimdallr, based on this master password, you can get a password for any website and account!
      <br />No more writing down tons of different passwords and trying to remember where they are, because they can be generated on the fly.
    </h4>
  </div>
</template>

<script>
export default {
  name: "InfoBlock"
};
</script>

<style scoped>
.info-block h2 {
  color: white;
}
.info-block h4 {
  color: rgba(255, 255, 255, 0.8);
  text-align: left;
}
</style>