-- Table: public.Order

-- DROP TABLE IF EXISTS public."Order";

CREATE TABLE IF NOT EXISTS public."Order"
(
    "Id" uuid NOT NULL,
    "UserId" uuid NOT NULL,
    "CreatedAt" timestamp without time zone NOT NULL,
    "DeletedAt" timestamp without time zone,
    "LastUpdateAt" timestamp without time zone,
    "Value" money NOT NULL
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."Order"
    OWNER to postgres;
-- Index: Id_index

-- DROP INDEX IF EXISTS public."Id_index";

CREATE UNIQUE INDEX IF NOT EXISTS "Id_index"
    ON public."Order" USING btree
    ("Id" ASC NULLS LAST)
    INCLUDE("Id")
    TABLESPACE pg_default;