import React from "react";
import { Link } from "react-router-dom";
import { CircularProgress, Button, Container, Grid } from "@mui/material";
import Stack from "@mui/material/Stack";
import styles from "./Office.module.css";

export const Office = () => {
  return (
    <>
      <Container>
        <Link to="/" style={{ textDecoration: "none" }}>
          <Stack direction="row" spacing={2}>
            <Button size="large">OSLO</Button>
          </Stack>
        </Link>
        <Button disabled size="large" style={{ textDecoration: "none" }}>
          DRAMMEN
        </Button>
      </Container>
    </>
  );
};
