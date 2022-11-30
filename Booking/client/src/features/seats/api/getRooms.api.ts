import { roomOneOslo, roomTwoOslo } from "../../../data/rooms";

export const fetchRoomOne = async () => {
  // når vi får api kallet
  // return axios.get('API_URL', { headers: { 'Accept': 'application/json' }})

  return roomOneOslo;
};

export const fetchRoomTwo = async () => {
  // når vi får api kallet
  // return axios.get('API_URL', { headers: { 'Accept': 'application/json' }})

  return roomTwoOslo;
};
