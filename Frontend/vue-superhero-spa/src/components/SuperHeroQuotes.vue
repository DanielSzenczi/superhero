<script>
  export default {
    name: 'SuperHeroQuotes',
  data() {
    return {
      hero : "",
      hero_quote: "",
    };
  },
  mounted() {
    this.GetQuotes();
    setInterval(this.GetQuotes, 10000); 
  },
  methods: {
    async GetQuotes() {
        try {
          const response = await fetch('http://localhost:7033/api/GetQuote'); //fetch data from azure function
          const data = await response.json();
    
          this.hero = data["name"];
          this.hero_quote = data["quote"]; //passing the result to the component variable


        } catch (error) {
            console.error('Error Fetching Superhero Quote:', error);
        }
    },
  },
};
</script>



<template>
    <h1>Name: {{ hero }} </h1>
    <h1>Quote: {{ hero_quote }}</h1>
</template>


  
