<template>
  <img alt="Batman" src="./assets/batman.png" width="128" height="132">
  <SuperHeroQuotes />
</template>

<script>
import SuperHeroQuotes from './components/SuperHeroQuotes.vue'

export default {
  name: 'App',
  components: {
    SuperHeroQuotes
  }
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
