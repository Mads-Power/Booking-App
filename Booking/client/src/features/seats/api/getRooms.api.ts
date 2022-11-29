import { roomsObject } from "../components/rooms";

export const fetchRoom = async () => {
  // når vi får api kallet
  // return axios.get('API_URL', { headers: { 'Accept': 'application/json' }})

  return roomsObject;
};
