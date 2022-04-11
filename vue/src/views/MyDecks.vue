<template>
    <div class="my-decks">
        <add-deck  />
        <list-decks />
    </div>
</template>

<script>
import AddDeck from '../components/AddDeck.vue'
import ListDecks from '../components/ListDecks.vue'
export default {
    name: 'my-decks',
    data(){
        return {
        }
    },
    components: {
        AddDeck,
        ListDecks,
    },
}
</script>