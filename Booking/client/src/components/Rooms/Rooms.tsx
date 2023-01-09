import { useRooms } from "@api/getRooms";
import { Box } from "@mui/system";
import { CircularProgress, Button, Container } from "@mui/material";
import useMediaQuery from "@mui/material/useMediaQuery";
import { Link } from "react-router-dom";
import { Seat } from "@type/seat";
import { WeekViewDatePicker } from "@components/WeekViewDatePicker";
import "./Rooms.module.css";
import { useEffect, useState } from "react";
import { useBookingsByRoom } from "@api/getBookingsByRoom";
import {
  DateContextType,
  useDateContext,
} from "@components/Provider/DateContextProvider";

export const Rooms = () => {
  const { isLoading, data, error } = useRooms();
  const { selectedDate: date, setSelectedDate: setDate }: DateContextType =
    useDateContext(); //'2023-01-02T13:42:16.115Z'
  const occupiedSeats = useBookingsByRoom(1, date);

  useEffect(() => {}, [date]);

  if (isLoading) {
    return <CircularProgress size={100} />;
  }

  if (error) {
    return <h4>No Rooms Found</h4>;
  }

  const renderIsOccupied = (seatId: number) => {
    const predicate =
      occupiedSeats.data?.find((o) => o.seatId == seatId) != undefined;
    return predicate;
  };

  const boxDefault = {
    display: "flex",
    flexWrap: "wrap",
    backgroundColor: "#CECECE",
    borderRadius: "25px",
    flexDirection: "row",
    p: 1,
    gap: 2,
    border: "1px solid",
    justifyContent: "space-around",
    placeItems: "center",
    minHeight: "30rem",
    maxWidth: "60rem",
  };

  return (
    <>
      <Box sx={{}}>
        <Box style={{ display: "flex", flexWrap: "wrap", width: "90vw" }}>
          <WeekViewDatePicker />
        </Box>
        {data?.map((rooms) => (
          <div key={rooms.id}>
            <Box>
              <h1>{rooms.name}</h1>
            </Box>
            <Box sx={boxDefault}>
              {rooms.seats.map((seat: Seat) => (
                <Link to={`/seat/${seat.id}`} key={seat.id} relative="path">
                  <Button
                    variant="contained"
                    sx={
                      renderIsOccupied(seat.id)
                        ? { backgroundColor: "#DF8B0D" }
                        : { backgroundColor: "#54A4D1" }
                    }
                  >
                    {seat.id}
                  </Button>
                </Link>
              ))}
            </Box>
          </div>
        ))}
      </Box>
    </>
  );
};
