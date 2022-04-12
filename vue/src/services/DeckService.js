import axios from 'axios';

export default {

  addDeck(deck) {
    return axios.post('/deck', deck)
  },
  myDecks(id){
      return axios.get(`/deck/mydecks/${id}`)
  },
}
