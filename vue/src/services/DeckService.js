import axios from 'axios';

export default {

  addDeck(deck) {
    return axios.post('/deck', deck)
  },
  myDecks(user){
      return axios.get('/deck/mydecks', user)
  },
}
