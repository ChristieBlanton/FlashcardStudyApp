import axios from 'axios';

export default {

  addCard(card) {
    return axios.post('/card', card)
  },
  getCardsInDeck(id){
      return axios.get(`/card/${id}`)
  },
}
