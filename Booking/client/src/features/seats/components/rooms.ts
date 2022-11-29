import { Room } from "../../../types";

export const roomsObject: Room = {
  id: 1,
  name: "rm1",
  capacity: 15,
  office: "Oslo",
  seats: [
    { id: "1", seatId: "1", isTaken: false },
    { id: "2", seatId: "2", isTaken: false },
    { id: "3", seatId: "3", isTaken: false },
    { id: "4", seatId: "4", isTaken: false },
    { id: "5", seatId: "5", isTaken: false },
    { id: "6", seatId: "6", isTaken: false },
    { id: "7", seatId: "7", isTaken: false },
    { id: "8", seatId: "8", isTaken: false },
  ],
};
