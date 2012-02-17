/*
* Script to initialize database, schemas and associated tables
* and constraints in correct sequence.
* Author:	Markus Vetsch
* Date:		20120213
*/

-- Create schema po

CREATE SCHEMA po
  AUTHORIZATION admin;
  
-- Grant schema access
  
GRANT USAGE ON SCHEMA po TO developer;
GRANT SELECT, INSERT, UPDATE, DELETE ON ALL TABLES IN SCHEMA po TO developer;

-- Create Table: po.player

CREATE TABLE po.player
(
  id SERIAL,
  firstname character varying(30) NOT NULL,
  lastname character varying(30) NOT NULL,
  birthday date NOT NULL,
  country character varying(2),
  CONSTRAINT pk_player PRIMARY KEY (id )
)
WITH (
  OIDS=FALSE,
  autovacuum_enabled=true
);
ALTER TABLE po.player
  OWNER TO admin;

-- Create Index: po.idx_player_id_firstname_lastname

CREATE INDEX idx_player_id_firstname_lastname
  ON po.player
  USING btree
  (id , firstname COLLATE pg_catalog."default" , lastname COLLATE pg_catalog."default" );

-- Create Table: "do".team

CREATE TABLE po.team
(
  id SERIAL,
  descriptor character(4) NOT NULL,
  agegroup character(10),
  validfrom date NOT NULL,
  validto date NOT NULL,
  CONSTRAINT pk_team PRIMARY KEY (id )
)
WITH (
  OIDS=FALSE,
  autovacuum_enabled=true
);
ALTER TABLE po.team
  OWNER TO admin;

-- Create Index: "do".idx_team_id_descriptorptor;

CREATE UNIQUE INDEX idx_team_id_descriptor
  ON po.team
  USING btree
  (id , descriptor COLLATE pg_catalog."default" );
  
-- Create Table: "do".perffeaturecategory

CREATE TABLE po.perffeaturecategory
(
  id bigint NOT NULL,
  nicename character(30) NOT NULL,
  CONSTRAINT pk_perffeaturecategory PRIMARY KEY (id )
)
WITH (
  OIDS=FALSE,
  autovacuum_enabled=true
);
ALTER TABLE po.perffeaturecategory
  OWNER TO admin;

-- Create Index: po.idx_perffeaturecategory_id_nicename

CREATE UNIQUE INDEX idx_perffeaturecategory_id_nicename
  ON po.perffeaturecategory
  USING btree
  (id , nicename COLLATE pg_catalog."default" );
  
-- Create Table: po.perfsubfeaturecategory

CREATE TABLE po.perfsubfeaturecategory
(
  id bigint NOT NULL,
  featurecategory_id integer NOT NULL,
  nicename character(30) NOT NULL,
  CONSTRAINT pk_perffeaturesubcategory PRIMARY KEY (id ),
  CONSTRAINT fk_perffeaturesubcategory_perffeaturecategory FOREIGN KEY (featurecategory_id)
      REFERENCES po.perffeaturecategory (id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE RESTRICT
)
WITH (
  OIDS=FALSE,
  autovacuum_enabled=true
);
ALTER TABLE po.perfsubfeaturecategory
  OWNER TO admin;

-- Create Index: po.idx_perffeaturesubcategory_id

CREATE INDEX idx_perffeaturesubcategory_id
  ON po.perfsubfeaturecategory
  USING btree
  (id );

-- Create schema ref
 
  CREATE SCHEMA ref
  AUTHORIZATION admin;
  
-- Grant schema access
  
GRANT USAGE ON SCHEMA ref TO developer;
GRANT SELECT, INSERT, UPDATE, DELETE ON ALL TABLES IN SCHEMA ref TO developer;
  
 -- Create table ref.playerreference
  
CREATE TABLE ref.playerreference
(
  id SERIAL,
  player_id bigint NOT NULL,
  team_id bigint NOT NULL,
  validfrom date NOT NULL,
  validto date NOT NULL,
  CONSTRAINT pk_playerreference PRIMARY KEY (id ),
  CONSTRAINT fk_player_playerreference FOREIGN KEY (player_id)
      REFERENCES po.player (id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE RESTRICT,
  CONSTRAINT fk_team_playerreference FOREIGN KEY (team_id)
      REFERENCES po.team (id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE RESTRICT
)
WITH (
  OIDS=FALSE,
  autovacuum_enabled=true
);
ALTER TABLE ref.playerreference
  OWNER TO admin;

-- Create Index: ref.idx_playerreference_id;

CREATE UNIQUE INDEX idx_playerreference_id
  ON ref.playerreference
  USING btree
  (id );

-- Create Table: po.perfmeasurement

CREATE TABLE po.perfmeasurement
(
  id SERIAL,
  playerreference_id bigint NOT NULL,
  value double precision,
  unit integer,
  "timestamp" timestamp without time zone NOT NULL,
  perfsubfeature_id integer NOT NULL,
  remark character varying(500),
  CONSTRAINT pk_perfmeasurement PRIMARY KEY (id ),
  CONSTRAINT fk_perfmeasurement_perfsubfeaturecategory FOREIGN KEY (perfsubfeature_id)
      REFERENCES po.perfsubfeaturecategory (id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE RESTRICT,
  CONSTRAINT fk_perfmeasurement_playerreference FOREIGN KEY (playerreference_id)
      REFERENCES ref.playerreference (id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE RESTRICT
)
WITH (
  OIDS=FALSE,
  autovacuum_enabled=true
);
ALTER TABLE po.perfmeasurement
  OWNER TO admin;

-- Create Index: po.fkidx_perfmeasurement_perfsubfeaturecategory

CREATE INDEX fkidx_perfmeasurement_perfsubfeaturecategory
  ON po.perfmeasurement
  USING btree
  (perfsubfeature_id );

-- Create Index: po.fkidx_perfmeasurement_playerref

CREATE INDEX fkidx_perfmeasurement_playerref
  ON po.perfmeasurement
  USING btree
  (playerreference_id );

-- Create Index: po.idx_perfmeasurement_id_playerreferenceid

CREATE INDEX idx_perfmeasurement_id_playerreferenceid
  ON po.perfmeasurement
  USING btree
  (id , playerreference_id );

-- Create schema mt
  
CREATE SCHEMA mt
  AUTHORIZATION admin;

-- Grant access
  
GRANT USAGE ON SCHEMA mt TO developer;
GRANT SELECT, INSERT, UPDATE, DELETE ON ALL TABLES IN SCHEMA mt TO developer;
  
-- Create Table: mt.playerdatahistory

CREATE TABLE mt.playerdatahistory
(
  id SERIAL,
  player_id integer NOT NULL,
  weight double precision,
  height integer,
  remark character varying(500),
  validfrom date NOT NULL,
  validto date NOT NULL,
  CONSTRAINT pk_playerdata PRIMARY KEY (id ),
  CONSTRAINT fk_player_id FOREIGN KEY (player_id)
      REFERENCES po.player (id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION
)
WITH (
  OIDS=FALSE,
  autovacuum_enabled=true
);
ALTER TABLE mt.playerdatahistory
  OWNER TO admin;

-- Create Index: mt.idx_playerdatahistory_id_playerid

CREATE INDEX idx_playerdatahistory_id_playerid
  ON mt.playerdatahistory
  USING btree
  (id , player_id );
