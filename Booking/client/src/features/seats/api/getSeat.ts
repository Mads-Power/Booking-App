import { useQuery } from "@tanstack/react-query";
import { Seats } from "../types";

const url = "http://localhost:51249/api/Seat";
export const getSeat = async () => {
  const res = await fetch(url);
  return res.json();
};

export const useSeat = () => {
  return useQuery<Seats[]>({
    queryKey: ["seat"],
    queryFn: () => getSeat(),
  });
};
