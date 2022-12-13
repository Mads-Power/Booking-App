import { useQuery, useMutation } from "@tanstack/react-query";
import { Booking } from "../types";

const url = "http://localhost:51249/api/Booking/Book";
export const putBookingBook = async (seatId: number, userId: number, date: string) => {
  const body = new URLSearchParams();
  body.append("seatId",seatId.toString());
  body.append("userId",userId.toString());
  body.append("date",date);
  console.log(body);
  const requestOptions = {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: body,
  };
  const res = await fetch(url, requestOptions);
  return res.json();
};

export const useBook = () => {
  return useMutation<Booking>({
    mutationFn: (seatId: number, userId: number, date: string) => putBookingBook(seatId, userId, date)
  });
};
