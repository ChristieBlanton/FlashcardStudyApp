<template>
    <div class="my-decks">
        <deck-details />
        <add-deck class="add-deck" />
        <list-decks class="list-decks" />
    </div>
</template>

<script>
import AddDeck from '../components/AddDeck.vue'
import ListDecks from '../components/ListDecks.vue'
import DeckDetails from '../components/DeckDetails.vue'

export default {
    name: 'my-decks',
    data(){
        return {
        }
    },
    components: {
        AddDeck,
        ListDecks,
        DeckDetails
    },
}
</script>

<style>

</style>