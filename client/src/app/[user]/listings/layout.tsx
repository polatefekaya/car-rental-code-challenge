import TopNavigationBar from "@/components/meui/top-navbar";
import { Text } from "@chakra-ui/react";
import { Metadata } from "next";
import { Suspense } from "react";

export const metadata: Metadata = {
    title: "Pekrent - Your Listings",

};

export default function RootLayout({
    children,
  }: Readonly<{
    children: React.ReactNode;
  }>){
    return(
        <>
            <TopNavigationBar page="yourlistings" authenticated={true}/>
            {children}
        </>
    );
}